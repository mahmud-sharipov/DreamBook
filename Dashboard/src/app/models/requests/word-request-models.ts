export class WordRequestModel {
    constructor(guid: string, translations: WordTranslationRequestModel[]) {
        this.guid = guid;
        this.translations = translations;
    }
    guid: string;
    translations: WordTranslationRequestModel[];
}

export class WordTranslationRequestModel {

    constructor(name: string, languageGuid: string) {
        this.languageGuid = languageGuid;
        this.name = name;
    }

    languageGuid: string;
    name: string;
}