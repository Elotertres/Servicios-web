import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{

  http = inject(HttpClient);
  title = 'Dating me';
  users: any;

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }
  setCurrentUser(){
    const userString = localStorage.getItem("user");
    if(!userString) return ;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }
  getUsers() {
    
    
    this.http.get("https://localhost:5001/api/v1/users").subscribe({
      next: (response) => {this.users = response},
      error: (error) => {console.log(error)},
      complete: () => {console.log("Request comlpete!")}
    });
  }
  
}
