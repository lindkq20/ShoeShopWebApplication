using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ShoeShopWeb.Models
{
    public class generalResponse
    {
        public ResponseArticles responseArticleSuccess(object articles, int numData)
        {
            ResponseArticles objSuccess = new ResponseArticles();
            objSuccess.success = true;
            objSuccess.articles = articles;
            objSuccess.total_elements = numData;

            return objSuccess;
        }

        public ResponseStores responseStoresSuccess(object stores, int numData)
        {
            ResponseStores objSuccess = new ResponseStores();
            objSuccess.success = true;
            objSuccess.stores = stores;
            objSuccess.total_elements = numData;

            return objSuccess;
        }

        public errorResponse responseError(HttpStatusCode error_code)
        {
            errorResponse objError = new errorResponse();
            objError.error_msg = messageError(error_code);
            objError.error_code = error_code;
            objError.success = false;

            return objError;
        }

        public string messageError(HttpStatusCode error_code)
        {
            errorResponse objError = new errorResponse();
            int idMessage = Convert.ToInt32(error_code);
            string message = "";

            switch (idMessage)
            {
                case 400:
                    message = errorResponse.error400;
                    break;
                case 401:
                    message = errorResponse.error401;
                    break;
                case 404:
                    message = errorResponse.error404;
                    break;
                case 500:
                    message = errorResponse.error500;
                    break;
                default:
                    message = "Fault occurred";
                    break;
            }

            return message;
        }
    }
}