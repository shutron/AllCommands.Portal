import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'search',
  templateUrl: './search.component.html'
})
export class SearchComponent {
  public results: SearchResult[];
  public searchText: string;
  public category: string;
  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }

  search() {
    this.http.get<SearchResult[]>(this.baseUrl + 'Search?category=' + this.category + "&searchText=" + this.searchText).subscribe(result => {
      this.results = result;
    }, error => console.error(error));
  }
}
interface SearchResult {
  command: string;
  example: string;
}

