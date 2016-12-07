export class AdvancedSearch {

  public genre: string;

  public imdb: number;
  public tomatometer: number;
  public metacritic: number;

  constructor(genre: string, imdb: number, tomatometer: number, metacritic: number) {
    this.genre = genre;

    this.imdb = imdb;
    this.tomatometer = tomatometer;
    this.metacritic = metacritic;
  }
}
