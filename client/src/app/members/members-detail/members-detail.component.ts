import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../models/member';
import {TabsModule} from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [TabsModule, GalleryModule],
  templateUrl: './members-detail.component.html',
  styleUrl: './members-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private memberService = inject(MembersService);
  member? : Member;
  images: GalleryItem[] = [];
  ngOnInit(): void {
  this.loadMember();
}
loadMember(){
  const username = this.route.snapshot.paramMap.get("username");
  if(!username) return;
  this.memberService.getMember(username).subscribe({
    next:(memberParam)=> {
      this.member = memberParam;
      this.member.photos.map(p => {
        this.images.push(new ImageItem({
          src: p.url,
          thumb: p.url,
        }
      )
    )
      }
    );
    }
  });
}
}