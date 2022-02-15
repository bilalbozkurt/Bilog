export interface User {
    id: number;
    username: string;
    firstName: string | null;
    lastName: string | null;
    email: string;
    gender: string | null;
    birthDate: string | null;
    role: string;
    timeCreated: string;
}