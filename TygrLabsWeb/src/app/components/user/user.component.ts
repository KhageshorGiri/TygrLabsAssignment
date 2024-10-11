import { Component, OnInit } from '@angular/core';
import { Users } from '../../interfaces/user.interface';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { UserService } from '../../services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

// Define an interface for the response structure
interface UserResponse {
  fields: Users;
}

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    HttpClientModule,
    CommonModule,
    RouterOutlet, RouterLink, RouterLinkActive,
    ReactiveFormsModule 
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
  providers: [UserService]
})
export class UserComponent implements OnInit {

  allusers: Users[] = [];
  userForm!: FormGroup;
  isNew : boolean = true;
  dynamicFields: string[] =  ['Email', 'Phone'];  // Initialize dynamic fields
  formTitle: string = "Add New";

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute) {
    this.formBuilder();
  }


  formBuilder() {
    this.userForm = this.fb.group({ // Initialize the form
      ID: ['', [Validators.required]],
      FirstName: ['', [Validators.required]],
      LastName: ['', [Validators.required]],
      dynamicFields: this.fb.array([]) // To hold dynamic fields
    });
  }

  ngOnInit(): void {
    this.getUsers();
    this.addDynamicFields();

    console.log(this.userForm.get('dynamicFields'));
  }

  addDynamicFields(): void {
    const dynamicFieldsArray = this.userForm.get('dynamicFields') as FormArray;
    this.dynamicFields.forEach(field => {
      dynamicFieldsArray.push(this.fb.control(''));
    });
  }

  addNewField(fieldName: string): void {
    const dynamicFieldsArray = this.userForm.get('dynamicFields') as FormArray;
    dynamicFieldsArray.push(this.fb.control(''));
    this.dynamicFields.push(fieldName);
  }

  getUsers(): void {
    this.userService.getUsers().subscribe(
      (response) => {
        // Check if users exist in the response
        if (response && response.users) {
          // Map through the users and extract the fields
          this.allusers = response.users.map((user: UserResponse) => user.fields);
        } else {
          console.error('No users found in the response');
        }
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      if (!this.isNew) {
        // Update user
        this.userService.editUser(this.userForm.value.ID, this.userForm.value).subscribe(
          response => {
            // Handle successful update
            console.log('User updated successfully', response);
            this.getUsers(); // Refresh the user list after update
          },
          error => {
            console.error('Error updating user', error);
          }
        );
      } else {
        // Create new user
        this.userService.addUser(this.userForm.value).subscribe(
          response => {
            // Handle successful creation
            console.log('User created successfully', response);
            this.getUsers(); // Refresh the user list after creation
          },
          error => {
            console.error('Error creating user', error);
          }
        );
      }
    }
  }

  deleteUser(id: number): void {

    var conformation = confirm('Are you sure?');

    if(conformation){
      this.userService.deleteUser(id).subscribe(
        () => {
          console.log('User deleted successfully');
          this.getUsers(); // Refresh the list of users
        },
        error => {
          console.error('Error deleting user:', error);
        }
      );
    }
    else{
      alert("Operation Cancled");
    }

  }

  cleanFormForNewUser(){
    this.formTitle = "Add New";
    this.userForm.patchValue({
      ID: "", // Adjust according to your response structure
      FirstName: "",
      LastName: "",
    });
  }

  loadUserData(id: number): void {
    this.userService.getUserById(id).subscribe(
      (response) => {
        if (response) {
          this.isNew = false;
          this.formTitle = "Edit Existing";
          // Assuming response has a fields property
          this.userForm.patchValue({
            ID: response.fields['ID'], // Adjust according to your response structure
            FirstName: response.fields['FirstName'],
            LastName: response.fields['LastName'],
          });
        } else {
          console.error('User not found');
        }
      },
      (error) => {
        console.error('Error fetching user data', error);
      }
    );
  }


}
