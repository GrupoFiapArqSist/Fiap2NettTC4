class ApiClient {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async get(endpoint, authToken = null) {
        const response = await this.send(endpoint, 'get', null, authToken)
        return response;
    }

    async post(endpoint, request, authToken = null) {
        const response = await this.send(endpoint, 'post', request, authToken)
        return response;
    }

    async put(endpoint, request, authToken = null) {
        const response = await this.send(endpoint, 'put', request, authToken)
        return response;
    }

    async send(endpoint, method, request = null, authToken = null) {
        const url = `${this.baseUrl}/${endpoint}`;

        const options = {
            method: method,
            headers: new Headers({
                'Authorization': authToken ? `Bearer ${authToken.accessToken}` : '',
                'Content-Type': 'application/json'
            }),
            body: request ? JSON.stringify(request) : null,
        };

        const data = await fetch(url, options);
        if (!data.ok) {
            throw Error(data);
        }
        const contentType = data.headers.get("content-type");
        if (contentType && contentType.indexOf("application/json") !== -1) {
            const response = await data.json();
            return response;
        };
    }

}