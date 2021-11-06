interface Scripts {
    name: string;
    src: string;
}
export const COLOR_PICKER_SCRIPT: string = 'ColorPicker';

export const ScriptStore: Scripts[] = [
    { name: COLOR_PICKER_SCRIPT, src: '../../asset/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js' }
];