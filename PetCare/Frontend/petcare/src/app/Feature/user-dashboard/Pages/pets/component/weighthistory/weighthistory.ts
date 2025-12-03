import { CommonModule, DatePipe } from '@angular/common'; // Import DatePipe
import { Component, inject, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { WeightHistoryService } from './service/weight-history-service';
import { IWeightHistory } from './type/weight-history.model';
import { ActivatedRoute } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AddWeightDialog } from './add-weight-dialog/add-weight-dialog';


@Component({
  selector: 'app-weighthistory',
  standalone: true,
  // 2. Add BaseChartDirective and DatePipe to imports
  imports: [CommonModule, FormsModule, MatButtonModule, MatDialogModule],
  providers: [DatePipe], // Add DatePipe to providers so we can use it in TS
  templateUrl: './weighthistory.html',
  styleUrls: ['./weighthistory.scss'],
})
export class Weighthistory {
  httpService = inject(WeightHistoryService);
  route = inject(ActivatedRoute);
  dialog = inject(MatDialog);
  datePipe = inject(DatePipe); // Inject DatePipe

  weightHistoryList: IWeightHistory[] = [];
  id: number = 0;



  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['petId'];
      this.httpService.getWeightHistory(this.id).subscribe(data => {
        this.weightHistoryList = data;
        this.updateChartData(); // <--- Update chart on load
      });
    });
  }

  // 4. Helper to refresh chart data
  updateChartData() {
    // Sort by date so the line goes left-to-right correctly
    const sortedData = [...this.weightHistoryList].sort((a, b) => 
      new Date(a.date).getTime() - new Date(b.date).getTime()
    );

   
  }

  deleteWeightHistory(whId: number) {
    this.httpService.deleteWeightHistory(whId).subscribe(() => {
      this.weightHistoryList = this.weightHistoryList.filter(wh => wh.whId !== whId);
      this.updateChartData(); // <--- Update chart on delete
    });
  }

  openDialog(weight?: IWeightHistory) {
    const dialogRef = this.dialog.open(AddWeightDialog, {
      width: '400px',
      data: weight || null
    });

    dialogRef.afterClosed().subscribe((result: IWeightHistory | null) => {
      if (!result) return;

      if (weight) {
        // ... (Edit Logic) ...
        const payload: Partial<IWeightHistory> = {
            petId: weight.petId,
            date: new Date(result.date),
            weightKg: result.weightKg
        };
        this.httpService.updateWeightHistory(weight.whId, payload).subscribe((updated) => {
          const index = this.weightHistoryList.findIndex((w) => w.whId === weight.whId);
          if (index > -1) this.weightHistoryList[index] = updated;
          this.updateChartData(); // <--- Update chart on Edit
        });
      } else {
        // ... (Add Logic) ...
        const payload: IWeightHistory = {
             whId: 0,
             petId: this.id,
             date: new Date(result.date),
             weightKg: result.weightKg
        };
        this.httpService.addWeightHistory(payload).subscribe((created) => {
          this.weightHistoryList.push(created);
          this.updateChartData(); // <--- Update chart on Add
        });
      }
    });
  }
}