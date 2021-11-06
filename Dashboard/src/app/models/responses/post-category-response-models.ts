import { LanguageShortResponseModel } from "./language-response-models";

export interface PostCategoryResponseModel {
    guid: string;
    name: string;
    description: string;
}

export interface PostCategoryTranslationResponseModel {
    language: LanguageShortResponseModel;
    guid: string;
    name: string;
    description: string;
}

export interface PostCategoryWithTranslationsResponseModel {
    guid: string;
    translations: PostCategoryTranslationResponseModel[];
}