export interface AttachmentResponse {
  attachId: number;        // unique ID of the attachment
  medicalEventId: number;  // related medical event ID
  fileUrl: string;         // URL/path to the uploaded file
  fileName: string;        // name of the file
  description: string;   
  isEditing: Boolean;// description text
}
