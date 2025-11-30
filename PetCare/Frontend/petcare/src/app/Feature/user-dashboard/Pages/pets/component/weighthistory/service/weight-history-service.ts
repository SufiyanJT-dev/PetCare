import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IWeightHistory } from '../type/weight-history.model';

@Injectable({
  providedIn: 'root',
})
export class WeightHistoryService {
  http = inject(HttpClient);
  apiUrl = 'https://localhost:7121/api/WeightHistory';
  getWeightHistory(petId: number) {
    return this.http.get<IWeightHistory[]>(`${this.apiUrl}/bypet/${petId}`);
}
  deleteWeightHistory(whId: number) {
    return this.http.delete(`${this.apiUrl}/delete/${whId}`);
  }
  addWeightHistory(weightHistory: IWeightHistory) {
    const payload = {
      ...weightHistory,
      date: new Date(weightHistory.date).toISOString(),
    } as any;
    return this.http.post<IWeightHistory>(`${this.apiUrl}/create`, payload);
  }
  updateWeightHistory(whId: number, weightHistory: Partial<IWeightHistory>) {
    const payload = {
      whId,
      ...weightHistory,
      date: weightHistory.date ? new Date(weightHistory.date).toISOString() : undefined,
    } as any;
    return this.http.put<IWeightHistory>(`${this.apiUrl}/update`, payload);
  }
}