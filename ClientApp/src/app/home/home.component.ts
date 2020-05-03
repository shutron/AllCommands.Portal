import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public results: SearchResult[];
  public searchText: string = "";
  public category: string = "";
  public categories: string[];
  public isLoading: boolean = false;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.http.get<string[]>(this.baseUrl + 'Search/GetCategories').subscribe(result => {
      this.categories = result;
    });
  }
  onChange(value) {

    this.isLoading = true;

    this.http.get<SearchResult[]>(this.baseUrl + 'Search?category=' + value + "&searchText=" + this.searchText).subscribe(result => {
      this.results = result;
      this.isLoading = false;

      setTimeout(() =>
        document.getElementById("resultContainer").scrollIntoView());
    }, error => {
      console.error(error);
      this.isLoading = false;
    });
  }
}

interface SearchResult {
  command: string;
  example: string;
}
