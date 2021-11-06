import { LanguageShortResponseModel } from "./language-response-models";

export interface DreamCategoryResponseModel {
    guid: string;
    name: string;
    description: string;
    color: string;
}

export interface DreamCategoryTranslationResponseModel {
    language: LanguageShortResponseModel;
    guid: string;
    name: string;
    description: string;
}

export interface DreamCategoryWithTranslationsResponseModel {
    color: string;
    guid: string;
    translations: DreamCategoryTranslationResponseModel[];
}