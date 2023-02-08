import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-listes',
  templateUrl: './listes.component.html',
  styleUrls: ['./listes.component.css']
})
export class ListesComponent implements OnInit {
  members: Member[] |undefined;
  predicate='liked';
  pageNumber=1;
  pageSize=5;
  pagination: Pagination|undefined;

  constructor(private memberServvice:MembersService) { }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes(){
    this.memberServvice.getLikes(this.predicate,this.pageNumber,this.pageSize).subscribe({
      next:response=>{
        this.members=response.result;
        this.pagination=response.pagination
      }
    })
  }

  pageChanged(event:any){
    if(this.pageNumber!==event.page){
      this.pageNumber=event.page;
      this.loadLikes();
    }
  }

}
