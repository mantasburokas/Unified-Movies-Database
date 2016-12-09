export class AdvancedSearch {

  public genre: string;

  public imdb: number;
  public tomatometer: number;
  public metacritic: number;

  public votes: number;

  constructor(genre: string, imdb: number, tomatometer: number, metacritic: number, votes: number) {
    this.genre = genre;

    this.imdb = imdb;
    this.tomatometer = tomatometer;
    this.metacritic = metacritic;

    this.votes = votes;
  }
}
