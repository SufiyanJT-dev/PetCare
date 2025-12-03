import { AbstractControl, ValidationErrors } from '@angular/forms';

export function minDate(maxDate: Date) {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const selectedDate = new Date(value);
    if (selectedDate <maxDate) {
      return { maxDate: true };
    }
    return null;
  };
}
