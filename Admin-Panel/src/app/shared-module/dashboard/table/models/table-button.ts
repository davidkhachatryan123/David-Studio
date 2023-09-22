export class TableButton {
  constructor(
    public btn_text: string,
    public expression: (value: string) => boolean,
    public click: (id: number | string, cell: string) => void,
  ) { }
}