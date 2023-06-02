import { TableButton } from "./table-button";
import { TableImage } from "./table-image";
import { TableText } from "./table-text";

export class TableConfiguration {
  private _type: TableText | TableButton | TableImage;

  get cell_type(): TableText | TableButton | TableImage {
    return this._type;
  }

  constructor(
    public type: TableText | TableButton | TableImage,
    public name: string,
    public title: string,
    public displayed: boolean
  ) {
    this._type = type;
  }
}