import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  @Output()
  public searched = new EventEmitter<string>();

  public value = '';


  constructor(
    private service: SearchService,
    private router: Router
  ) {
  }

  public async checkForEnter(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.searched.emit(this.value);
      await this.service.getResults(this.value); // loading
      this.router.navigate(['../search-results']);
    }
  }
}
