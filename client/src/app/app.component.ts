import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { HomeComponent } from "./home/home.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavComponent, HomeComponent],
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
  setCurrentUser(): void{
    const userString = localStorage.getItem("user");
    if(!userString) return ;
    const user = JSON.parse(userString);
    this.accountservice.currentUser.set(user);
  }
  getUsers() {
    
    
    this.http.get("https://localhost:5001/api/v1/users").subscribe({
      next: (response) => {this.users = response},
      error: (error) => {console.log(error)},
      complete: () => {console.log("Request comlpete!")}
    });
  }
  
}
