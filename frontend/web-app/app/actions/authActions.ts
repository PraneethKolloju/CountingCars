import { auth } from '@/auth'

export async function getCurrentUser() {
    try {
        var user = await auth();
        if (user)
            return user.user
        else
            return null
    } catch (error) {
        console.log("error in getCurrentUser", error)
        return null
    }
}
