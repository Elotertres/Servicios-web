import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../models/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  standalone: true,

  imports: [MemberCardComponent,MemberCardComponent],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})

export class MemberListComponent implements OnInit{
  private memberService = inject(MembersService);
  members: Member[] = [];
  ngOnInit(): void {
   this.loadMembers();
  }
  loadMembers(){
    this.memberService.getMembers()
    .subscribe({
      next: members=>this.members = members
    })
  }

}