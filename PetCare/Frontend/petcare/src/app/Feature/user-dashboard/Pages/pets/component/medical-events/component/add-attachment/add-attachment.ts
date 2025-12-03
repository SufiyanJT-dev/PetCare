import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AttachmentService } from './service/attachment';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-attachment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-attachment.html',
  styleUrl: './add-attachment.scss',
})
export class AddAttachment {

  id: number = 0;
  attachments: any[] = [];

  // Form fields
  attachmentId: number | null = null;  // For edit
  fileName: string = "";
  description: string = "";
  file: File | null = null;
  isEditMode = false;
showForm = false;
  attachmentApi = inject(AttachmentService);
  route = inject(ActivatedRoute);

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['Id'];
      this.loadAttachments();
    });
  }
toggleForm() {
  this.showForm = !this.showForm;

  // Reset form when hiding
  if (!this.showForm) {
    this.resetForm();
  }
}
  loadAttachments() {
    this.attachmentApi.getAllByMedicalId(this.id).subscribe({
      next: (value) => {
        this.attachments = value;
        console.log("Attachments:", value);
      }
    });
  }

  // Handle file upload
  onFileChange(event: any) {
    this.file = event.target.files[0] ?? null;
  }

  // Add or Update
  saveAttachment() {
    let formData = new FormData();

    formData.append("fileName", this.fileName);
    formData.append("description", this.description);

    if (this.file) {
      formData.append("file", this.file);
    }
     console.log(formData);

    if (this.isEditMode && this.attachmentId) {   
       formData.append("medicalEventId", this.id.toString());
      this.attachmentApi.updateMedical(this.attachmentId, formData).subscribe({
        next: () => {
          this.resetForm();
          this.loadAttachments();
        }
      });
    } else {
      // Add new
      formData.append("medicalEventId", this.id.toString());

      this.attachmentApi.addMedical(formData).subscribe({
        next: () => {
          this.resetForm();
          this.loadAttachments();
        }
      });
    }
  }

  // Edit attachment
  editAttachment(a: any) {
    this.isEditMode = true;
    this.attachmentId = a.attachId;
    this.fileName = a.fileName;
    this.description = a.description;
    this.file = null;
 this.showForm = true;
  }

  // Delete attachment
  deleteAttachment(id: number) {
    if (!confirm("Delete this attachment?")) return;

    this.attachmentApi.deleteMedical(id).subscribe(() => {
      this.attachments = this.attachments.filter(x => x.attachId !== id);
    });
  }

  // Reset form
  resetForm() {
    this.isEditMode = false;
    this.attachmentId = null;
    this.fileName = "";
    this.description = "";
    this.file = null;
  }
}
