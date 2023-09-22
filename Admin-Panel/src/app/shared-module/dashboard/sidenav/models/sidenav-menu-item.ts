import { ListItem } from "./list-item";

export interface SidenavMenuItem {
  title: string;
  material_icon: string;

  listItems: Array<ListItem>;
}
