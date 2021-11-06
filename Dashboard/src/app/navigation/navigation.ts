import { NavigationItemModel, NavigationType } from "./NavigationItemModel";

export const MENU: NavigationItemModel[] = [
    {
        id: 'home',
        title: 'Главная',
        type: NavigationType.Item,
        icon: 'fas fa-home',
        url: 'home',
        children: []
    },
    {
        id: 'books',
        title: 'Сонник',
        type: NavigationType.Item,
        icon: 'fas fa-book',
        url: 'books',
        children: []
    },
    {
        id: 'words',
        title: 'Слова',
        type: NavigationType.Item,
        icon: 'fas fa-tags',
        url: 'words',
        children: []
    },
    {
        id: 'dream-categories',
        title: 'Категории снов',
        type: NavigationType.Item,
        icon: 'fas fa-list',
        url: 'dream-categories',
        children: []
    },
    {
        id: 'ad',
        title: 'Релама',
        type: NavigationType.Item,
        icon: 'fas fa-ad',
        url: 'ads',
        children: []
    },
    {
        id: 'posts',
        title: 'Статьи',
        type: NavigationType.Item,
        icon: 'fas fa-newspaper',
        url: 'posts',
        children: []
    },
    {
        id: 'post-categories',
        title: 'Категории статей',
        type: NavigationType.Item,
        icon: 'fas fa-list',
        url: 'post-categories',
        children: []
    },
    {
        id: 'statistics',
        title: 'Статистика',
        type: NavigationType.Item,
        icon: 'fas fa-chart-area',
        url: 'statistics',
        children: []
    },
    {
        id: 'users',
        title: 'Пользователи',
        type: NavigationType.Item,
        icon: 'fas fa-users',
        url: 'users',
        children: []
    }
];
