/*執行 新增按鈕觸發事件 */
function add_list() {

    /*取得要寫入資料庫的資料 */
    var name = document.getElementById("name").value;
    var age = document.getElementById("age").value;

    /*Post MVC Controller 裡面的 function*/
    $.ajax(
        {
            url: "addItem",
            method: 'POST',
            data: {
                name: name,
                age: age
            },
            dataType: 'json',
            success: function (response) {

                //成功事件提示
                if (response == "success") {

                    alert("Add Completed");
                }
                //失敗事件提示
                else {
                    return "error";
                }
            },
            //Post 失敗提示
            error: function (xhr, status, error) {
                debugger;
                return alert(error);
                //return alert("Add Failure\n");

            }
        });

}