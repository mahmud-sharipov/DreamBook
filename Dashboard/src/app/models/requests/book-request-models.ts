export class BookRequestModel {
    constructor(guid: string, translations: BookTranslationRequestModel[]) {
        this.guid = guid;
        this.translations = translations;
    }
    guid: string;
    translations: BookTranslationRequestModel[];
}

export class BookTranslationRequestModel {

    constructor(name: string, description: string, languageGuid: string) {
        this.languageGuid = languageGuid;
        this.name = name;
        this.description = description;
    }

    languageGuid: string;
    description: string;
    name: string;
}