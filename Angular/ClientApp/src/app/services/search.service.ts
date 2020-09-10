import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ElasticDoc } from '../models/elasticDoc';

@Injectable()
export class SearchService {
    public results : ElasticDoc[];
    constructor(private http: HttpClient) {
    }

    public async getResults(searchQuery: string): Promise<ElasticDoc[]> {
        return new Promise<ElasticDoc[]>((resolve) => {
            this.http.post('https://localhost:5001/Search', { rawQuery: searchQuery }).subscribe((result: ElasticDoc[]) => {
                this.results = result;
                resolve(result);
            });
        });
    }
}
