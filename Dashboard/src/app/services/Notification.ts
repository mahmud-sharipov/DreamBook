import Swal from 'sweetalert2';

export enum NotificationSeverity {
    success = 'success',
    error = 'error',
    warning = 'warning',
    info = 'info',
    query = 'question'
}

export enum SweetAlertInput {
    text = 'text',
    email = 'email',
    password = 'password',
    number = 'number',
    tel = 'tel',
    range = 'range',
    textarea = 'textarea',
    select = 'select',
    radio = 'radio',
    checkbox = 'checkbox',
    file = 'file',
    url = 'url',
}

export enum SweetAlertPosition {
    top = 'top',
    top_start = 'top-start',
    top_end = 'top-end',
    top_left = 'top-left',
    top_right = 'top-right',
    center = 'center',
    center_start = 'center-start',
    center_end = 'center-end',
    center_left = 'center-left',
    center_right = 'center-right',
    bottom = 'bottom',
    bottom_start = 'bottom-start',
    bottom_end = 'bottom-end',
    bottom_left = 'bottom-left',
    bottom_right = 'bottom-right',
}

export enum SweetAlertUpdatableParameters {
    allowEscapeKey = 'allowEscapeKey',
    allowOutsideClick = 'allowOutsideClick',
    background = 'background',
    buttonsStyling = 'buttonsStyling',
    cancelButtonAriaLabel = 'cancelButtonAriaLabel',
    cancelButtonColor = 'cancelButtonColor',
    cancelButtonText = 'cancelButtonText',
    closeButtonAriaLabel = 'closeButtonAriaLabel',
    closeButtonHtml = 'closeButtonHtml',
    confirmButtonAriaLabel = 'confirmButtonAriaLabel',
    confirmButtonColor = 'confirmButtonColor',
    confirmButtonText = 'confirmButtonText',
    currentProgressStep = 'currentProgressStep',
    customClass = 'customClass',
    denyButtonAriaLabel = 'denyButtonAriaLabel',
    denyButtonColor = 'denyButtonColor',
    denyButtonText = 'denyButtonText',
    didClose = 'didClose',
    didDestroy = 'didDestroy',
    footer = 'footer',
    hideClass = 'hideClass',
    html = 'html',
    icon = 'icon',
    iconColor = 'iconColor',
    imageAlt = 'imageAlt',
    imageHeight = 'imageHeight',
    imageUrl = 'imageUrl',
    imageWidth = 'imageWidth',
    preConfirm = 'preConfirm',
    preDeny = 'preDeny',
    progressSteps = 'progressSteps',
    reverseButtons = 'reverseButtons',
    showCancelButton = 'showCancelButton',
    showCloseButton = 'showCloseButton',
    showConfirmButton = 'showConfirmButton',
    showDenyButton = 'showDenyButton',
    text = 'text',
    title = 'title',
    titleText = 'titleText',
    willClose = 'willClose',
}

export const Toast = Swal.mixin({
    toast: true,
    position: SweetAlertPosition.top_end,
    showConfirmButton: false,
    timer: 1500,
    timerProgressBar: true
});