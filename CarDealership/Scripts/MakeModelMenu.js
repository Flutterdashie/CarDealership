function UpdateModels() {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:55792/api/admin/models/' + $('#carMake').val(),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                success: function (data) {
                    $('#carModel').empty();
                    $('#carModel').append('<option value="-2" disabled selected>Model</option>');
                    $.each(data,
                        function (index, item) {
                            var option = '<option value="' + item.ModelID + '">' + item.ModelName + '</option>';
                            $('#carModel').append(option);
                        });
                    $('#carModel').attr('disabled', false);
                },
                error: function (jqXHR) {
                    $('#carModel').attr('disabled', true);
                    alert('the model getter broke and said: ' + jqXHR.responseText);
                }
            });
        }

        function UpdateMakes() {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:55792/api/admin/makes/',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                success: function(data) {
                    $('#carMake').empty();
                    $('#carMake').append('<option value="-2" disabled selected>Make</option>');
                    $.each(data,
                        function(index, item) {
                            var option = '<option value="' + item.MakeID + '">' + item.MakeName + '</option>';
                            $('#carMake').append(option);
                        });
                    $('#carMake').attr('disabled', false);
                },
                error: function(jqXHR) {
                    $('#carMake').attr('disabled', true);
                    alert('the make getter broke and said: ' + jqXHR.responseText);
                }
            });
        }
