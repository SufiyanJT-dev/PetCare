import { Component, inject } from '@angular/core';
import { UpcomingReminderService } from './service/upcoming-reminder-service';
import { GetIdServices } from '../../../../Shared/Service/get-id-services';
import { IUpcomingReminders } from './type/upcoming-reminders.model';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-upcoming-reminder',
  imports: [DatePipe,CommonModule],
  templateUrl: './upcoming-reminder.html',
  styleUrl: './upcoming-reminder.scss',
})
export class UpcomingReminder {
  httpService = inject(UpcomingReminderService);
  getIdServices = inject(GetIdServices);
  userId = this.getIdServices.getUserID();
  upcomingRemindersList: IUpcomingReminders[] = [];
  ngOnInit() {
 
this.getUpcomingReminders(this.userId);
}
 getUpcomingReminders(userId: number) {
    return this.httpService.getUpcomingReminders(userId).subscribe((data) => {
      this.upcomingRemindersList = data;
      console.log(this.upcomingRemindersList);
    });
  }
}