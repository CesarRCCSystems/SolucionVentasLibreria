/* 
 * Se Requiere : 
 * JQuery, JQuery-UI, JQuery-DataTables, JQuery-LeadingOverlay
 * Toastr.js, Sweetalert2.js,  
*/

//*********** Mensajes Informativos ************//

    ////// Toastr /////
    var tOptions = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.options = tOptions;

    const Toastr = () => {
        toastr.options = tOptions;
        toastr.error('Have fun storming the castle!', 'Miracle Max Says')
    };  

    ////// SweetAlert2 /////
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
    });

    const MsgSaveSuccess = () => {
        Toast.fire({
            icon: 'success',
            title: 'Guardado correctamente.'
        })
    }

    const MsgSuccessSE = (valor) => {
            Toast.fire({
                icon: 'success',
                title: valor == 0 ? 'Guardado correctamente.' : 'Editado correctamente.'
            })
        }

    const MsgEditSuccess = () => {
        Toast.fire({
            icon: 'success',
            title: 'Editado correctamente.'
        })
    }

    const MsgError = (msg) => {
        if (msg != null) {
            Toast.fire({
                icon: 'error',
                title: !msg.startsWith("#") ? msg : 'Ha ocurrido un error.'
            });
            console.log(msg);
        } else {
            console.log('MSGError, el parametro msg es nulo!!');
        }
        }

    //// Eliminacion Advertencia ////
    const SwalDelete = ({ Title, then }) => {

        Swal.fire({
            title: Title,
            text: 'Este proceso no podrá ser revertido',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#1199ae',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, elimínalo',
            cancelButtonText: 'No, Cancelar',
        }).then((result) => then(result));
    }

    //// Eliminacion Exitosa ////
    const MsgToast = (msg) => {
        $(document).Toasts('create', {
            class: 'bg-success',
            title: 'Eliminación Exitosa',
            subtitle: 'Eliminado',
            body: msg
        });
    };

/////******** Mensajes de Carga (Loading) *********/////

    const ShowLoading = (tloading) => {

        let values = {
            image: ImageUrl,
            imageResizeFactor: 4,
            imageAnimation: false,
            text: 'Cargando...'
        };

        if (tloading != null) {

                values.size = 14;

                $(tloading).LoadingOverlay('show', values );
        } else {

                values.size = 12;
                values.textColor = "#1199ae";

                $.LoadingOverlay("show", values );
         }
    };

    const HideLoading = (tloading) => {

        if (tloading != null) {
            $(tloading).LoadingOverlay('hide');
        } else {
            $.LoadingOverlay('hide')
        }

    };

//*********** JQuery DOM Mostrar Títulos  ************//

    const Title = (name) => {
    $('.titulo1').text(name);
    $('.titulo2').text(name);
}

    const ModalTitle = ({ Fontawesome, Title }) => {
        $('.modal-title').append($('<i>').addClass(Fontawesome), `  ${Title}`);
    };

//********** Trabajo con DataTables ***********//

    const GetDataTable = ({ TableId, Url, Columns }) => {

        colsToBeCentered = [];
        for (i = 1; i <= 2; i++) {
            colsToBeCentered.push($(TableId + " th").length - i);
        }

        let table = $(TableId).DataTable({
            "responsive": true,
            //"lengthChange": false,
            "autoWidth": false,
            "ajax": {
                url: Url,
                type: 'GET',
                dataType: 'json'
            },
            "columns": Columns,
            "columnDefs": [{
                className: "text-center", "targets": colsToBeCentered
            }],
            "language": {
                "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
            }
        });

        return table;
    }

    const GetRow = ({ table, ctx }) => {

        //var MarcaSeleccionada = $(this).closest('tr');

        //if (MarcaSeleccionada.hasClass('child')) {
        //    MarcaSeleccionada = MarcaSeleccionada.prev();
        //}

        let row = $(ctx).parents('tr').hasClass('child') ?
            $(ctx).parents().prev('tr') :
            $(ctx).parents('tr');

        let result = {
            row: row,
            data: table.row(row).data()
        }

        return result;
    }

//*********** Peticiones de AJAX ************//

    const AjaxPost = ({ url, obj, data, success, tloading }) => {

        let AjaxOject = {
            url: url,
            type: 'POST',
            cache: false,
            /*data: JSON.stringify(obj),*/
                     
            success: (data) => {
                HideLoading(tloading);
                success(data);
            },
            error: (error) => {
                HideLoading(tloading);
                MsgError(etag + error);   
                console.log(error);
            },
            beforeSend: () => {
                ShowLoading(tloading);
         
            }
        }

        if (data != null) {
            AjaxOject.data = data;
            AjaxOject.processData = false;
            AjaxOject.contentType = false;
        }

        if (obj != null) {
            AjaxOject.data = JSON.stringify(obj);
            AjaxOject.contentType = contentType;
            AjaxOject.dataType = 'json';
        }

        $.ajax(AjaxOject);
    }

     const AjaxGet = ({ url, success, tloading }) => {

        jQuery.ajax({
            url: url,
            type: "GET",
            cache: false,
            dataType: "json",
            contentType: contentType,
            success: (data) => {
                HideLoading(tloading);
                success(data);
            },
            error: (error) => {     
                HideLoading(tloading);
                MsgError(etag + error);    
            },
            beforeSend: () => {
                ShowLoading(tloading);
            },
        });
    }

//***************** Fechas *********************//

//**** JQuery UI DatePicker Traducido a Español ****//

    const DatepickerInSpanish = () => {

        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '< Ant',
            nextText: 'Sig >',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };

        $.datepicker.setDefaults($.datepicker.regional['es']);
    };

//**** Obtener fecha actual (clase "Date") formato DD/MM/YYYY ****//

    const GetDate = () => {

        let d = new Date();
        let month = d.getMonth() + 1;
        let day = d.getDate();
        let output = (('' + day).length < 2 ? '0' : '')     +  day  + '/' +
                     (('' + month).length < 2 ? '0' : '')   + month + '/' +
                     d.getFullYear();

        return output;
    }


// llamada POST mediante Fetch (aun no funciona testear) //
    const FetchPost = ({ url, obj, success, tloading }) => {
          
        let config = {
            method: 'POST', // or 'PUT'
            body: JSON.stringify(obj), // data can be `string` or {object}!
            headers: {
                'Content-Type': 'application/json'
            }
        };

        ShowLoading();

        fetch(url, config)
            .then(response => {
                   
                debugger;
                if (response.ok) {

                    return  response.json()
                } else {
                    return response.error(); 
                }

            }).catch(error => {
                HideLoading(tloading);
                //MsgError(error);
                console.log(error)
            }).then(data => {
                debugger;
                HideLoading(tloading);
                console.log()
                success(data);
            });
    };

/***************** Chart JS  ******************/

const LoadChart = ({ canvasId, Type, labels, datasets }) => {
        console.log(JSON.stringify(config[Type]));
    if (labels != null && datasets != null) {
        let data = config[Type].data;

        data.labels = labels;
        data.datasets[0] = { ...data.datasets[0], ...datasets };
        //$.extend(true, data.datasets[0], datasets);

        } 

        console.log(JSON.stringify(config[Type]));
        var ChartCanvas = $(canvasId).get(0).getContext('2d')

        let chart = new Chart(ChartCanvas, config[Type]);

        return chart;
    };