export class Tile {
  public constructor (
    public subtitle: string = '',
    public price: number = 0,
    public decription_list_translation_keys: Array<string> = []
  ) { }
}