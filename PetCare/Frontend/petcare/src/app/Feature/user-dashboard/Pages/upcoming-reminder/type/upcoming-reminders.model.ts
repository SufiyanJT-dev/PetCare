export interface IUpcomingReminders {
    reminderId: number;
    petId: number;
    petName: string;
    dateTime: Date;
    type: string;
    description: string;
    isCompleted: boolean;
}