export class DreamCategoryRequestModel {
    constructor(guid: string, color: string, translations: DreamCategoryTranslationRequestModel[]) {
        this.guid = guid;
        this.translations = translations;
        this.color = color;
    }
    guid: string;
    color: string;
    translations: DreamCategoryTranslationRequestModel[];
}

export class DreamCategoryTranslationRequestModel {

    constructor(name: string, description: string, languageGuid: string) {
        this.languageGuid = languageGuid;
        this.name = name;
        this.description = description;
    }

    languageGuid: string;
    description: string;
    name: string;
}