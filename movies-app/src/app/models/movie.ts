export class Movie {

  public title: string;

  public released: string;

  public imdbRating: string;

  public metascore: string

  public tomatoMeter: string;

  public imdbVotes: string;

  public plot: string;

  public runtime: string;

  public director: string;

  public awards: string;

  constructor (title: string, released: string,
               imdbRating: string, metascore: string,
               tomatoMeter: string, imdbVotes: string,
               plot: string, runtime: string,
               director: string, awards: string) {
    this.title = title;

    this.released = released;

    this.imdbRating = imdbRating;

    this.metascore = metascore;

    this.tomatoMeter = tomatoMeter;

    this.imdbVotes = imdbVotes;

    this.plot = plot;

    this.runtime = runtime;

    this.director = director;

    this.awards = awards;
  }
}
