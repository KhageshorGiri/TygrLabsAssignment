
export interface Users{
    ID: number;
    FirstName: string,
    LastName: string,
    [key: string]: any; // This allows for dynamic fields
}