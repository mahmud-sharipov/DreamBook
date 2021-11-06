import { LanguageShortResponseModel } from "./language-response-models";

export interface WordResponseModel {
    guid: string;
    name: string;
}

export interface WordWithTranslationsResponseModel {
    guid: string;
    translations: WordTranslationResponseModel[];
}

export interface WordTranslationResponseModel {
    language: LanguageShortResponseModel;
    guid: string;
    name: string;
}

export interface WordInterpretationResponseModel {
    guid: string;
    book: string;
    bookGuid: string;
    description: string;
}