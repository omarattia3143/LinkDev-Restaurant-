export default function authHeader() {

    // @ts-ignore
    const jwt = localStorage.getItem("jwt");

    console.log(jwt);
    if (jwt) {
        return { Authorization: 'Bearer ' + jwt };
    } else {
        return {};
    }
}