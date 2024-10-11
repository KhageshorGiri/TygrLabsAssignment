import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7149/api/users'; 
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  // Get all users and columns
  getUsers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetAllUsers`, this.httpOptions);
  }

  // Get user by ID
  getUserById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetUsrById/${id}`, this.httpOptions);
  }

  // Add a new user
  addUser(user: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/AddUser`, JSON.stringify(user), this.httpOptions);
  }

  // Add a new column
  addColumn(columnName: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/AddColumn`, JSON.stringify(columnName), this.httpOptions);
  }

  // Edit a user
  editUser(id: number, user: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/EditUser/${id}`, JSON.stringify(user), this.httpOptions);
  }

  // Delete a user
  deleteUser(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/DeleteUser/${id}`, this.httpOptions);
  }
}
