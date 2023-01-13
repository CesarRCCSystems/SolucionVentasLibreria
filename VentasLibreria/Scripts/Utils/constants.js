
    ////********** Iconos Usados ***********//
    //const { fontawesome, bootstrapIcons } = {
    //    "fontawesome": {
    //        "Menu": {
    //            "Dashboard": "<i class='fas fa-tachometer-alt'></i>",
    //            "Almacen": "<i class='fas fa-tools'></i>",
    //            "Compras": "<i class='fas fa-cart-arrow-down'></i>",
    //            "Ventas": "<i class='fas fa-cash-register'></i>",
    //            "Reportes": "<i class='fas fa-clipboard'></i>",
    //            "Acceso": "<i class='fas fa-user-lock'></i>"
    //        },
    //        "SubMenu": {
    //            "Marca": "<i class='fas fa-bookmark'></i>",
    //            "Categorias": "<i class='fab fa-wpforms'></i>",
    //            "Productos": "<i class='fas fa-box-open'></i>",
    //            "Proveedores": "<i class='fas fa-shipping-fast'></i>",
    //            "RegistrarCompra": "<i class='fas fa-cart-arrow-down'></i>",
    //            "ConsultarCompra": "<i class='far fa-list-alt'></i>",
    //            "AsignarProductoTienda": "<i class='fas fa-dolly'></i>",
    //            "Clientes": "<i class='fas fa-user-shield'></i>",
    //            "RegistrarVenta": "<i class='fas fa-cash-register'></i>",
    //            "ConsultarVenta": "<i class='fas fa-clipboard'></i>",
    //            "ProductosXtienda": "<i class='fas fa-boxes'></i>",
    //            "Ventas": "<i class='fas fa-shopping-basket'></i>",
    //            "Tiendas": "<i class='fas fa-store-alt'></i>",
    //            "Rol": "<i class='fas fa-user-tag'></i>",
    //            "AsignarPermisos": "<i class='fas fa-user-lock'></i>",
    //            "Usuarios": "<i class='fas fa-users-cog'></i>"
    //        },
    //        "Acciones": {
    //            "Editar1": "<i class='fas fa-edit'></i>",
    //            "Editar2": "<i class='fas fa-pen'></i>",
    //            "Eliminar": "<i class='fa fa-trash'></i>",
    //            //"Guardar": "<i class='fa fa-edit'>",
    //            //"Excel": "<i class='fa fa-edit'>",
    //            //"Pdf": "<i class='fa fa-edit'>"
    //        }
    //    },
    //    "bootstrapIcons": {
    //        "Acciones": {
    //            "Eliminar": "<i class='bi bi-trash3-fill'></i>"
    //        }
    //    },
    //    "ionicons": {

    //    },
    //    "useiconic": {

    //    }

    //};

    //********** Iconos Usados ***********//
    const { fontawesome, bootstrapIcons } = {
        "fontawesome": {
            "faMenu": {
                "Dashboard": "fas fa-tachometer-alt",
                "Almacen": "fas fa-tools",
                "Compras": "fas fa-cart-arrow-down",
                "Ventas": "fas fa-cash-register",
                "Reportes": "fas fa-clipboard",
                "Acceso": "fas fa-user-lock"
            },
            "faSubMenu": {
                "Marca": "fas fa-bookmark",
                "Categorias": "fab fa-wpforms",
                "Productos": "fas fa-box-open",
                "Proveedores": "fas fa-shipping-fast",
                "RegistrarCompra": "fas fa-cart-arrow-down",
                "ConsultarCompra": "fas fa-list-alt",
                "AsignarProductoTienda": "fas fa-dolly",
                "Clientes": "fas fa-user-shield",
                "RegistrarVenta": "fas fa-cash-register",
                "ConsultarVenta": "fas fa-clipboard",
                "ProductosXtienda": "fas fa-boxes",
                "Ventas": "fas fa-shopping-basket",
                "Tiendas": "fas fa-store-alt",
                "Rol": "fas fa-user-tag",
                "AsignarPermisos": "fas fa-user-lock",
                "Usuarios": "fas fa-users-cog"
            },
            "faAcciones": {
                "Editar1":  "fas fa-pen",
                "Editar2": "fas fa-edit",
                "Eliminar1": "fas fa-trash",
                "Eliminar2": "fas fa-trash-alt",
                "Eliminar3": "fas fa-trash-restore",
                "Eliminar4": "fas fa-trash-restore-alt",
                "Guardar": "fas fa-save",
                "Agregar": "fas fa-plus-circle",
            },
            "faOtros": {
                "Spider": "fab fa-instagram-square"
            }
        },
        "bootstrapIcons": {
            "bAcciones": {
                "Eliminar": "bi bi-trash3-fill",
                "Editar": "bi bi-pencil-square",
                "Excel": "bi bi-file-earmark-excel-fill",
                "Check": "bi bi-check-square-fill",
                "Guardar": "bi bi-save"
            }
        },
        "ionicons": {

        },
        "useiconic": {

        }

    };

    const faForm = {
        "nombre": "",
        "telefono": "fas fa-phone",
        "direccion": "",
        "RUC": "",
        "correo": "fas fa-envelope",
        "password": "",
        "nrodocumento": "",
        "nrodocumento": "",
        "usuario": "",
        "facha": "far fa-calendar-alt"
    };


    const { faAcciones, faOtros } = fontawesome;

    //************ Botones e Insignias *************//
    const { btnEditar, btnEliminar, badgeActivo, badgeNoActivo } = {
    "btnEditar": "<button class='btn btn-info btn-sm btn-editar'><i class='" + faAcciones.Editar2 + "'></i></button>",
            "btnEliminar": "<button class='btn btn-danger btn-sm ms-2 btn-eliminar'><i class='" + bootstrapIcons.bAcciones.Eliminar + "'></i></button>",
            "badgeActivo": '<span class="badge badge-success">Activo</span>',
            "badgeNoActivo": '<span class="badge badge-danger">No Activo</span>'
            };

    //*********** Columnas de DataTable **************//

    const { AED } = {
        "AED": [{
            "data": "Activo",
            "render": function (value) {
                return value ? badgeActivo : badgeNoActivo
             }
             },
                {
                    "defaultContent": btnEditar + btnEliminar,
                    "orderable": false,
                    "searchable": false,
                    "width": "90px"
                }]
    };

    const pValid = (rangelength) => {
   
        let pValid = {
            required: true,
            normalizer: function (value) {
                return $.trim(value);
            }
        };

        if (rangelength != null) {
            pValid.rangelength = rangelength
        }

        //if (minlength != null) {
        //    pValid.minlength = minlength;
        //}


        //if (maxlength != null) {
        //    pValid.maxlength = maxlength;
        //}

        //if (param != null) {

        //let { int } = param;

        //if (int) {
        //    pValid.digits = true;
        //}
        
        //}
       
        return pValid
    } 

    const etag = "# ";

    //*********  Parametros para hacer peticiones json *********//
    const contentType = 'application/json; charset=utf-8';


    //******* Chartjs Configuraciones *******// 

    const ctype = {
        area: 'area', //tambien es line pero con fondo
        stacked: 'stacked', 
        bar: 'bar',
        line: 'line',
        bubble: 'bubble',
        doughnut: 'doughnut',
        pie: 'pie',
        polarArea: 'polarArea',
        radar: 'radar',
        scatter: 'scatter',
        barcolors: "barcolors"
    }
    
    //const cdata = {
    //    label: months({ config: 12 }),
    //    fill: false,
    //    borderColor: Utils.CHART_COLORS.red,
    //    backgroundColor: Utils.transparentize(Utils.CHART_COLORS.red, 0.5),
    //}
    
    const coptions = {
    
    }


//********** Chart Area Line **********//

   // var areaChartCanvas = $('#areaChart').get(0).getContext('2d')

    var areaChartData = {
    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
    datasets: [
        {
            label: 'Digital Goods',
            backgroundColor: 'rgba(60,141,188,0.9)',
            borderColor: 'rgba(60,141,188,0.8)',
            pointRadius: false,
            pointColor: '#3b8bba',
            pointStrokeColor: 'rgba(60,141,188,1)',
            pointHighlightFill: '#fff',
            pointHighlightStroke: 'rgba(60,141,188,1)',
            data: [28, 48, 40, 19, 86, 27, 90]
        },
        {
            label: 'Electronics',
            backgroundColor: 'rgba(210, 214, 222, 1)',
            borderColor: 'rgba(210, 214, 222, 1)',
            pointRadius: false,
            pointColor: 'rgba(210, 214, 222, 1)',
            pointStrokeColor: '#c1c7d1',
            pointHighlightFill: '#fff',
            pointHighlightStroke: 'rgba(220,220,220,1)',
            data: [65, 59, 80, 81, 56, 55, 40]
        },
    ]
}

    var areaChartOptions = {
    maintainAspectRatio: false,
    responsive: true,
    legend: {
        display: false
    },
    scales: {
        xAxes: [{
            gridLines: {
                display: false,
            }
        }],
        yAxes: [{
            gridLines: {
                display: false,
            }
        }]
    }
}

//********** Chart Line **********//

   // var lineChartCanvas = $('#lineChart').get(0).getContext('2d')
    var lineChartOptions = $.extend(true, {}, areaChartOptions)
    var lineChartData = $.extend(true, {}, areaChartData)
    lineChartData.datasets[0].fill = false;
    lineChartData.datasets[1].fill = false;
    lineChartOptions.datasetFill = false;

//********** Chart DONUT **********//

   // var donutChartCanvas = $('#donutChart').get(0).getContext('2d')
    var donutData = {
        labels: [
            'Chrome',
            'IE',
            'FireFox',
            'Safari',
            'Opera',
            'Navigator',
        ],
        datasets: [
            {
                data: [700, 500, 400, 600, 300, 100],
                backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de'],
            }
        ]
    }
    var donutOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }

//********** Chart PIE **********//

    //var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
    var pieData = donutData;
    var pieOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }


//********** Chart BAR **********//

   // var barChartCanvas = $('#barChart').get(0).getContext('2d')
    var barChartData = $.extend(true, {}, areaChartData)
    var temp0 = areaChartData.datasets[0]
    var temp1 = areaChartData.datasets[1]
    barChartData.datasets[0] = temp1
    barChartData.datasets[1] = temp0

    var barChartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        datasetFill: false
    }

//********** Chart STACKED BAR **********//

    //var stackedBarChartCanvas = $('#stackedBarChart').get(0).getContext('2d')
    var stackedBarChartData = $.extend(true, {}, barChartData)

    var stackedBarChartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            xAxes: [{
                stacked: true,
            }],
            yAxes: [{
                stacked: true
            }]
        }
    }


//************ Chart  Colors bar  *************//

    var colorData = {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 7, 3],
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1
        }]
    };

var colorOptions = {
    legend: {
        display: false
    },
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }; 

//*********************************//

    const config = {

        area: {
            type: 'line',
            data: areaChartData,
            options: areaChartOptions
        },
        line: {
            type: 'line',
            data: lineChartData,
            options: lineChartOptions
        },
        doughnut: {
            type: 'doughnut',
            data: donutData,
            options: donutOptions
        },
        pie: {
            type: 'pie',
            data: pieData,
            options: pieOptions
        },
        bar: {
            type: 'bar',
            data: barChartData,
            options: barChartOptions
        },
        stacked: {
            type: 'bar',
            data: stackedBarChartData,
            options: stackedBarChartOptions
        },
        barcolors: {
            type: 'bar',
            data: colorData,
            options: colorOptions
        }
        //bubble: 'bubble',
        //polarArea: 'polarArea',
        //radar: 'radar',
        //scatter: 'scatter',
    };

