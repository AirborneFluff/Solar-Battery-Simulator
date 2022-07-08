export interface User {
  id: number;
  defaultSystemId: number,
  token: string;
}
export interface UserLogin {
  email: string;
  password: string;
}
