export interface NavigationItemModel {
    id: string;
    title: string;
    type: NavigationType;
    icon: string;
    url: string;
    children: NavigationItemModel[];
}

export enum NavigationType {
    Group,
    Collapsable,
    Item
}