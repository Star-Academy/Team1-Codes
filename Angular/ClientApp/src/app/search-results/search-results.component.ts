import {Component, OnInit} from '@angular/core';
import { SearchService } from '../services/search.service';
import { ElasticDoc } from '../models/elasticDoc';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html'
})
export class SearchResultsComponent implements OnInit{
  public documents: ElasticDoc[];

  constructor(private service : SearchService) {
  }

  async ngOnInit() {
    // this.documents = await this.service.getResults("get");
    this.documents = this.service.results;
  }
}
