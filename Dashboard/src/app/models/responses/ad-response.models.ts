import { LanguageShortResponseModel } from "./language-response-models";

export interface AdResponseModel {
    guid: string;
    image: string;
    createdAt: string;
    source: string;
    title: string;
    isActive: boolean;
    description: string;
}

export interface AdTranslationResponseModel {
    language: LanguageShortResponseModel;
    guid: string;
    title: string;
    description: string;
}

export interface AdWithTranslationsResponseModel {
    guid: string;
    image: string;
    createdAt: string;
    source: string;
    isActive: boolean;
    translations: AdTranslationResponseModel[];
}