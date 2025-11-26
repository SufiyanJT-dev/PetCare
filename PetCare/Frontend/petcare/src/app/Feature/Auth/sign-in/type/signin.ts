export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;       // JWT or any token returned by backend
  userName?: string;   // optional, if backend returns user info
  role?: string;       // optional
}
