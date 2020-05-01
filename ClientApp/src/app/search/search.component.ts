import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'search',
  templateUrl: './search.component.html'
})
export class SearchComponent {
  public results: SearchResult[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<SearchResult[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.results = result;
    }, error => console.error(error));
  }
}

interface SearchResult {
  command: string;
  example: string;
}
