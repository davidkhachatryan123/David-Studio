export class Service {
  public constructor(
    public title_translation_key: string = '',
    public body_translation_key: string = '',
    public image_uri: string = '',
    public image_pos: 'left' | 'right' = 'left',
    public route: string = '/',
  ) { }
}