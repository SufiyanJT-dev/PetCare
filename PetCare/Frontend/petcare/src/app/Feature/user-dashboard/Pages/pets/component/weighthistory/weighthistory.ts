import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
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
  imports: [CommonModule, FormsModule, MatButtonModule, MatDialogModule],
  templateUrl: './weighthistory.html',
  styleUrls: ['./weighthistory.scss'],
})
export class Weighthistory {
  httpService = inject(WeightHistoryService);
  route = inject(ActivatedRoute);
  dialog = inject(MatDialog);

  weightHistoryList: IWeightHistory[] = [];
  newWeightHistory: IWeightHistory = {
    whId: 0,
    petId: 0,
    date: new Date(),
    weightKg: 0
  };
  id: number = 0;

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['petId'];
      this.httpService.getWeightHistory(this.id).subscribe(data => {
        this.weightHistoryList = data;
      });
    });
  }

  deleteWeightHistory(whId: number) {
    this.httpService.deleteWeightHistory(whId).subscribe(() => {
      this.weightHistoryList = this.weightHistoryList.filter(wh => wh.whId !== whId);
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
        const payload: Partial<IWeightHistory> = {
          petId: weight.petId,
          date: new Date(result.date),
          weightKg: result.weightKg
        };

        this.httpService.updateWeightHistory(weight.whId, payload).subscribe((updated: IWeightHistory) => {
          const index = this.weightHistoryList.findIndex((w: IWeightHistory) => w.whId === weight.whId);
          if (index > -1) this.weightHistoryList[index] = updated;
        });
      } else {
        const payload: IWeightHistory = {
          whId: 0,
          petId: this.id,
          date: new Date(result.date),
          weightKg: result.weightKg
        };

        this.httpService.addWeightHistory(payload).subscribe((created: IWeightHistory) => {
          this.weightHistoryList.push(created);
        });
      }
    });
  }
}
