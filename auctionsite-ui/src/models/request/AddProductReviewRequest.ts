export default interface AddProductReviewRequest {
    productId: number
    userId: string
    content: string
    score: number
}