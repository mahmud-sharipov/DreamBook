export class PostCategoryRequestModel {
    constructor(guid: string, translations: PostCategoryTranslationRequestModel[]) {
        this.guid = guid;
        this.translations = translations;
    }
    guid: string;
    translations: PostCategoryTranslationRequestModel[];
}

export class PostCategoryTranslationRequestModel {

    constructor(name: string, description: string, languageGuid: string) {
        this.languageGuid = languageGuid;
        this.name = name;
        this.description = description;
    }

    languageGuid: string;
    description: string;
    name: string;
}