import { LanguageShortResponseModel } from "./language-response-models";

export interface BookResponseModel {
    guid: string;
    name: string;
    description: string;
}

export interface BookTranslationResponseModel {
    language: LanguageShortResponseModel;
    guid: string;
    name: string;
    description: string;
}

export interface BookWithTranslationsResponseModel {
    guid: string;
    translations: BookTranslationResponseModel[];
}