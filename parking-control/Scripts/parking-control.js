function ParkingControl() {}
ParkingControl.prototype = {
    Init: function () {
        $("#listIndex .delete-index").on("click",
            function (e) {                
                e.preventDefault();
                if (confirm("Deseja excluir permanentemente o registro?")) {
                    var url = $(this).attr("href");                    
                    window.location = url;
                }
            });

    }

};

var parkingControl = new ParkingControl();
parkingControl.Init();