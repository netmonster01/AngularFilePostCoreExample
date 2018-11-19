export interface User {
  id: number;
  email: string;
  userName: string;
  password: string;
  firstName: string;
  lastName: string;
  avatarImage: string;
  roles: string[];
  token: string;
  isAdmim: boolean;
}
