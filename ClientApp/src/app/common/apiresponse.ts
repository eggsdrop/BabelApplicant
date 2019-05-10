/**
 * Class representing JSON ApiResponse's
 * @class
 */
export class ApiResponse {
    /**
     * Indicates Success, Failure or Warning
     */
    status: string;

    /**
     * Optional message returned with the API response
     */
    message: string;

    /**
     * Data payload returned with the response
     */
    data: any;

    /**
     * API response has a success status
     */
    isSuccess() {
        return this.status === 'Success';
    }

    /**
     * API response was successful and contains data
     */
    isData() {
        return this.isSuccess() && this.data && this.data.length > 0;
    }
}
