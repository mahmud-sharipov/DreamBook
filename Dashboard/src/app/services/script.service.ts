import { Injectable } from "@angular/core";
import { Observable, Observer } from "rxjs";
import { ScriptStore } from "../helpers/scrip-model";

declare var document: any;

@Injectable({
    providedIn: 'root'
})
export class ScriptService {

    private scripts: ScriptModel[] = [];

    public load(script: ScriptModel): Observable<ScriptModel> {
        return new Observable<ScriptModel>((observer: Observer<ScriptModel>) => {
            var existingScript = this.scripts.find(s => s.name == script.name);

            // Complete if already loaded
            if (existingScript && existingScript.loaded) {
                observer.next(existingScript);
                observer.complete();
            }
            else {
                // Add the script
                this.scripts = [...this.scripts, script];

                // Load the script
                let scriptElement = document.createElement("script");
                scriptElement.type = "text/javascript";
                scriptElement.src = script.src;

                scriptElement.onload = () => {
                    script.loaded = true;
                    observer.next(script);
                    observer.complete();
                };
                scriptElement.onerror = (error: any) => {
                    observer.error("Couldn't load script " + script.src);
                };

                document.getElementsByTagName('body')[0].appendChild(scriptElement);
            }
        });
    }
}

export interface ScriptModel {
    name: string,
    src: string,
    loaded: boolean
}