import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IUpcomingReminders } from '../type/upcoming-reminders.model';

@Injectable({
  providedIn: 'root',
})
export class UpcomingReminderService {
  http = inject(HttpClient);

  getUpcomingReminders(id: number) {
    return this.http.get<IUpcomingReminders[]>(`https://localhost:7121/api/Reminders/user/${id}/upcoming`);
  }
}
